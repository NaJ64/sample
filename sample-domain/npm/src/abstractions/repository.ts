import { IAggregate } from './aggregate';

export interface IRepository<TAggregate extends IAggregate<TKey>, TKey=number> {
    getAsync(): Promise<TAggregate[]>;
    getAsync(skip: number, take: number): Promise<TAggregate[]>;
    getAsync(id: TKey): Promise<TAggregate | null>;
    insertAsync(record: TAggregate): Promise<TAggregate>;
    updateAsync(id: TKey, record: TAggregate): Promise<TAggregate>;
    deleteAsync(id: TKey): Promise<void>;
}