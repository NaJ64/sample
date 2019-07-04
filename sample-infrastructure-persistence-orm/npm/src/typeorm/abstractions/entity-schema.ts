import { EntitySchemaColumnOptions } from "typeorm";

export const EntityBaseSchemaColumns = {
  id: {
    type: Number,
    primary: true,
    generated: true,
  } as EntitySchemaColumnOptions
};