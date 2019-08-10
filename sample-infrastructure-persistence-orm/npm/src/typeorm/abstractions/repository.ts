import { IEntity, IRepository } from "sample-domain";
import { Repository } from "typeorm";

export abstract class TORepositoryBase<TEntity extends IEntity<TKey>, TRaw extends IEntity<TKey> = TEntity, TKey=number> implements IRepository<TEntity, TKey> {

    protected readonly _getRepository: () => Repository<TRaw>;

    constructor(getRepository: () => Repository<TRaw>) {
        let lazyInstance: Repository<TRaw> | null = null;
        this._getRepository = () => {
            return lazyInstance || (lazyInstance = getRepository());
        }
    }

    protected abstract hydrate(json: Partial<TRaw>): TEntity;

    getAsync(): Promise<TEntity[]>;    
    getAsync(id: TKey): Promise<TEntity | null>;
    getAsync(skip: number, take: number): Promise<TEntity[]>;
    async getAsync(skipOrId?: number | TKey, take?: number): Promise<TEntity[] | TEntity | null> {
        if (typeof take == "number") {
            if (skipOrId == null) {
                throw new Error("Value for 'skip' must be be provided");
            }
            const [result] = await this._getRepository().findAndCount({
                take: take,
                skip: <number>skipOrId
            });
            return result.map(x => this.hydrate(x));
        }
        if (skipOrId == null) {
            return (await this._getRepository().find()).map(x => this.hydrate(x));
        }
        const findOneResult = await this._getRepository().findOne(<TKey>skipOrId) || null;
        return (!findOneResult) ? null : this.hydrate(findOneResult);
    }

    async insertAsync(record: TEntity): Promise<TEntity> {
        const repo = this._getRepository();
        const saveResult = await repo.save(<any>record) as TRaw;
        return this.hydrate(saveResult);
        //const newId: number = insertResult.identifiers[0]["id"];
        // const result =  await repo.findOne(newId);
        // if (!result) { 
        //     throw new Error(`No record found with inserted id ${newId}`);
        // }
        // return this.hydrate(result);
    }

    async updateAsync(id: TKey, record: TEntity): Promise<TEntity> {
        const result = await this._getRepository().save(<any>record) as TRaw;
        return this.hydrate(result);
    }

    async deleteAsync(id: TKey): Promise<void> {
        const result = await this._getRepository().findOneOrFail(id);
        await this._getRepository().remove(result);
    }
    
}