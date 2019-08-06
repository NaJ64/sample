import { EntitySchemaColumnOptions } from "typeorm";

export const EntityBaseSchemaColumns = {
  id: {
    type: Number,
    primary: true,
    generated: "increment",
  } as EntitySchemaColumnOptions
};