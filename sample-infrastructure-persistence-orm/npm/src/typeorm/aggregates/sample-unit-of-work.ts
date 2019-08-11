import { inject, injectable } from "inversify";
import { IParentRepository, ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { Connection, ConnectionOptions } from "typeorm";
import { TYPES as OrmPersistence } from "../../dependency-injection/types";
import { TOUnitOfWorkBase, TOUnitOfWorkFactoryBase } from "../abstractions/unit-of-work";
import { ChildSchema } from "./parent/child-schema";
import { TOParentRepository } from "./parent/parent-repository";
import { ParentSchema } from "./parent/parent-schema";

export class TOSampleUnitOfWork extends TOUnitOfWorkBase implements ISampleUnitOfWork {

    static readonly DEFAULT_SCHEMA = "sample";

    readonly parents: IParentRepository;

    constructor(getConnection: () => Connection) {
        super(getConnection);
        this.parents = new TOParentRepository(
            () => this.getRepositoryForSchema(ParentSchema), 
            () => this.getRepositoryForSchema(ChildSchema)
        );
    }

}

@injectable()
export class TOSampleUnitOfWorkFactory extends TOUnitOfWorkFactoryBase<TOSampleUnitOfWork> implements ISampleUnitOfWorkFactory {

    constructor(@inject(OrmPersistence.TypeORM.ConnectionOptions) connectionOptions: ConnectionOptions) {
        super(connectionOptions);
    }

    protected createInstance(getConnection: () => Connection): TOSampleUnitOfWork {
        return new TOSampleUnitOfWork(getConnection);
    }

}