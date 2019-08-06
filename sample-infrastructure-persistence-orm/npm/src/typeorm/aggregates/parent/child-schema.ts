import { EntitySchema } from "typeorm";
import { Child, Parent } from "sample-domain";
import { EntityBaseSchemaColumns } from "../../abstractions/entity-schema";

export const ChildSchema = new EntitySchema<Child & { _parent: Parent }>({
    name: "child",
    columns: {
        ...EntityBaseSchemaColumns,
        parentId: {
            type: Number,
        },
        description: {
            type: String
        }
    },
    relations: {
        _parent: {
            type: "many-to-one",
            target: "parent",
            joinColumn: {
                name: "parentId"
            },
            onDelete: "CASCADE",
            onUpdate: "CASCADE"
        }
    }
});