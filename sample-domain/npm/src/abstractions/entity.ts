export interface IEntity<TKey = number> {
    id: TKey;
}

export abstract class EntityBase implements IEntity<number> {
    id: number;
    constructor() {
        this.id = 0;
    }
}