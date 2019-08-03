import { IEntity, IRepository } from "sample-domain";
import { Repository } from "typeorm";

export abstract class TORepositoryBase<TEntity extends IEntity<TKey>, TKey=number> implements IRepository<TEntity, TKey> {

    protected readonly _getRepository: () => Repository<TEntity>;

    constructor(getRepository: () => Repository<TEntity>) {
        let lazyInstance: Repository<TEntity> | null = null;
        this._getRepository = () => {
            return lazyInstance || (lazyInstance = getRepository());
        }
    }

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
            return result;
        }
        if (skipOrId == null) {
            return await this._getRepository().find();
        }
        return await this._getRepository().findOne(<TKey>skipOrId) || null;
    }

    async insertAsync(record: TEntity): Promise<TEntity> {
        const result = await this._getRepository().save(<any>record);
        return result;
    }

    async updateAsync(id: TKey, record: TEntity): Promise<TEntity> {
        const result = await this._getRepository().save(<any>record);
        return result;
    }

    async deleteAsync(id: TKey): Promise<void> {
        const result = await this._getRepository().findOneOrFail(id);
        await this._getRepository().remove(result);
    }
    
}