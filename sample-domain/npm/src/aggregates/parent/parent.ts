import { Child, IChild } from "./child";
import { AggregateBase } from "../../abstractions/aggregate";
import { IEntity } from "../../abstractions/entity";

export interface IParent extends IEntity {
    description: string;
    children: IChild[];
}

export class Parent extends AggregateBase {
    description: string;
    children: Child[];
    constructor(description: string) {
        super();
        this.description = description || "";
        this.children = [];
    }
    hydrate(state: Partial<IParent>): Parent {
        if (!state) {
            state = {};
        }
        this.id = state.id || 0;
        this.description = state.description || "";
        this.children = (state.children || []).map(x => new Child(0, "").hydrate(x));
        return this;
    }
    addChild(description: string) {
        const child = new Child(this.id, description);
        this.children.push(child);
        return child;
    }
    removeChild(child: Child) {
        const found = this.children.indexOf(child);
        if (found >= 0) {
            this.children.splice(found, 1);
        }
    }
}