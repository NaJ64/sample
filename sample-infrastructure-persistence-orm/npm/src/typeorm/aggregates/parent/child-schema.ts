import { EntitySchema } from "typeorm";
import { Child } from "sample-domain";
import { EntityBaseSchemaColumns } from "../../abstractions/entity-schema";

export const ChildSchema = new EntitySchema<Child>({
    name: "child",
    target: Child,
    columns: {
        ...EntityBaseSchemaColumns,
        parentId: {
            type: Number,
        },
        description: {
            type: String
        }
    }
});