import { EntityBase, IEntity } from "../../abstractions/entity";

export interface IChild extends IEntity {
    parentId: number;
    description: string;
}

export class Child extends EntityBase implements IChild {
    parentId: number;
    description: string;
    constructor(parentId: number, description: string) {
        super();
        this.parentId = parentId;
        this.description = description;
    }
    hydrate(state: Partial<IChild>): Child {
        this.id = state.id || 0;
        this.parentId = state.parentId || 0;
        this.description = state.description || "";
        return this;
    }
}