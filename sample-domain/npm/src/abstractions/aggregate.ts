import { IEntity, EntityBase } from "./entity";

export interface IAggregate<TKey=number> extends IEntity<TKey> { }

export abstract class AggregateBase extends EntityBase implements IAggregate<number> {}