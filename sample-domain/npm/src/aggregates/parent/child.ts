import { EntityBase } from "../../abstractions/entity";

export class Child extends EntityBase {
    parentId: number;
    description: string;
    constructor(parentId: number, description: string) {
        super();
        this.parentId = parentId;
        this.description = description;
    }
}