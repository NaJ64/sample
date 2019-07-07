import { IParentRepository, Parent } from "sample-domain";
import { Repository } from "typeorm";
import { RepositoryBase } from "../../abstractions/repository";

export class ParentRepository extends RepositoryBase<Parent> implements IParentRepository {

    constructor(getRepository: () => Repository<Parent>) {
        super(getRepository);
    }

}