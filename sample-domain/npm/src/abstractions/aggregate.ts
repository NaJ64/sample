import { IEntity, EntityBase } from "./entity";

export interface IAggregate<TKey=number> extends IEntity<TKey> { }

export class AggregateBase extends EntityBase implements IAggregate<number> {}