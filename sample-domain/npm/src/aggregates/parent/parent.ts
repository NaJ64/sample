import { Child } from "./child";
import { AggregateBase } from "../../abstractions/aggregate";

export class Parent extends AggregateBase {
    description: string;
    children: Child[];
    constructor(description: string) {
        super();
        this.description = description || "";
        this.children = [];
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