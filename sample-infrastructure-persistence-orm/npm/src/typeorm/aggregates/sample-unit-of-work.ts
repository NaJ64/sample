import { IParentRepository, ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { Connection, ConnectionOptions } from "typeorm";
import { TOUnitOfWorkBase, TOUnitOfWorkFactoryBase } from "../abstractions/unit-of-work";
import { TOParentRepository } from "./parent/parent-repository";
import { ParentSchema } from "./parent/parent-schema";
import { injectable, inject } from "inversify";
import { TYPES as OrmPersistence } from "../../dependency-injection/types";

export class TOSampleUnitOfWork extends TOUnitOfWorkBase implements ISampleUnitOfWork {

    readonly parents: IParentRepository;

    constructor(getConnection: () => Connection) {
        super(getConnection);
        this.parents = new TOParentRepository(() => this.getRepositoryForSchema(ParentSchema));
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