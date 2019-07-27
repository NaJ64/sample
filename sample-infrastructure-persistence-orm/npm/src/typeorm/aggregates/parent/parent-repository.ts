import { IParentRepository, Parent } from "sample-domain";
import { Repository } from "typeorm";
import { TORepositoryBase } from "../../abstractions/repository";

export class TOParentRepository extends TORepositoryBase<Parent> implements IParentRepository {

    constructor(getRepository: () => Repository<Parent>) {
        super(getRepository);
    }

}