import { EntitySchema } from "typeorm";
import { Parent } from "sample-domain";
import { EntityBaseSchemaColumns } from "../../abstractions/entity-schema";

export const ParentSchema = new EntitySchema<Parent>({
    name: "parent",
    target: Parent,
    columns: {
        ...EntityBaseSchemaColumns,
        description: {
            type: String
        }
    },
    relations: {
        children: {
            type: "one-to-many",
            target: "child",
            joinColumn: {
                name: "parentId"
            },
            cascade: true
        }
    }
});