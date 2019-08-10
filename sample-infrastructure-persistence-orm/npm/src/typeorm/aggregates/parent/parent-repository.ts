import { IParentRepository, Parent, IParent } from "sample-domain";
import { Repository } from "typeorm";
import { TORepositoryBase } from "../../abstractions/repository";

export class TOParentRepository extends TORepositoryBase<Parent, IParent> implements IParentRepository {

    constructor(getRepository: () => Repository<IParent>) {
        super(getRepository);
    }

    protected hydrate(json: Partial<IParent>): Parent {
        return new Parent("").hydrate(json);
    }

}