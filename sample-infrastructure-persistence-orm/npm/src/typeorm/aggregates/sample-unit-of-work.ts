import { IParentRepository, ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { Connection, ConnectionOptions } from "typeorm";
import { UnitOfWorkBase, UnitOfWorkFactoryBase } from "../abstractions/unit-of-work";
import { ParentRepository, ParentSchema } from "./parent";
import { injectable, inject } from "inversify";
import { TYPES as OrmPersistence } from "../../dependency-injection/types";

export class SampleUnitOfWork extends UnitOfWorkBase implements ISampleUnitOfWork {

    readonly parents: IParentRepository;

    constructor(getConnection: () => Connection) {
        super(getConnection);
        this.parents = new ParentRepository(() => this.getRepositoryForSchema(ParentSchema));
    }

}

@injectable()
export class SampleUnitOfWorkFactory extends UnitOfWorkFactoryBase<SampleUnitOfWork> implements ISampleUnitOfWorkFactory {

    constructor(@inject(OrmPersistence.TypeORM.ConnectionOptions) connectionOptions: ConnectionOptions) {
        super(connectionOptions);
    }

    protected createInstance(getConnection: () => Connection): SampleUnitOfWork {
        return new SampleUnitOfWork(getConnection);
    }

}