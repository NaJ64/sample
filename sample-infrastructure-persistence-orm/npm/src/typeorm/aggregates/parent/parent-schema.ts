import { EntitySchema } from "typeorm";
import { Parent } from "sample-domain";
import { EntityBaseSchemaColumns } from "../../abstractions/entity-schema";

export const ParentSchema = new EntitySchema<Parent>({
    name: "parent",
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
            inverseSide: "_parent",
            cascade: true,
            eager: true
        }
    }
});