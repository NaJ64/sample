import { IEntity, IRepository } from "sample-domain";
import { Repository } from "typeorm";

export abstract class RepositoryBase<TEntity extends IEntity<TKey>, TKey=number> implements IRepository<TEntity, TKey> {

    protected readonly _repository: Repository<TEntity>;

    constructor(repository: Repository<TEntity>) {
        this._repository = repository;
    }

    getAsync(): Promise<TEntity[]>;    
    getAsync(id: TKey): Promise<TEntity | null>;
    getAsync(skip: number, take: number): Promise<TEntity[]>;
    async getAsync(skipOrId?: number | TKey, take?: number): Promise<TEntity[] | TEntity | null> {
        if (typeof take == "number") {
            if (skipOrId == null) {
                throw new Error("Value for 'skip' must be be provided");
            }
            const [result] = await this._repository.findAndCount({
                take: take,
                skip: <number>skipOrId
            });
            return result;
        }
        return await this._repository.findOne(<TKey>skipOrId) || null;
    }

    async insertAsync(record: TEntity): Promise<TEntity> {
        const result = await this._repository.save(<any>record);
        return result;
    }

    async updateAsync(id: TKey, record: TEntity): Promise<TEntity> {
        const result = await this._repository.save(<any>record);
        return result;
    }

    async deleteAsync(id: TKey): Promise<void> {
        const result = await this._repository.findOneOrFail(id);
        await this._repository.remove(result);
    }
    
}