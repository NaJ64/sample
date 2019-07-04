export interface IRepository<TEntity, TKey=number> {
    getAsync(): Promise<TEntity[]>;
    getAsync(skip: number, take: number): Promise<TEntity[]>;
    getAsync(id: TKey): Promise<TEntity | null>;
    insertAsync(record: TEntity): Promise<TEntity>;
    updateAsync(id: TKey, record: TEntity): Promise<TEntity>;
    deleteAsync(id: TKey): Promise<void>;
}