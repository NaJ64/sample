import { IParentRepository, Parent, IParent, IChild, Child } from "sample-domain";
import { Repository } from "typeorm";
import { TORepositoryBase } from "../../abstractions/repository";

export class TOParentRepository extends TORepositoryBase<Parent, IParent> implements IParentRepository {

    protected readonly _getChildRepository: () => Repository<IChild>;

    constructor(getRepository: () => Repository<IParent>, getChildRepository: () => Repository<IChild>) {
        super(getRepository);
        let lazyInstance: Repository<IChild> | null = null;
        this._getChildRepository = () => {
            return lazyInstance || (lazyInstance = getChildRepository());
        }
    }

    protected hydrate(json: Partial<IParent>): Parent {
        return new Parent("").hydrate(json);
    }

    async insertAsync(record: Parent): Promise<Parent> {
        // Do two-phase insert (split parent/children entities)
        const children = record.children.splice(0);
        const insertedParent = await super.insertAsync(record);
        children.forEach(x => x.parentId = insertedParent.id);
        const childRepo = this._getChildRepository();
        if (children.length) {
            const childResults = await childRepo.save(children);
            for(let childResult of childResults) {
                insertedParent.children.push(new Child(childResult.parentId, childResult.description).hydrate(childResult));
            }
        }
        return insertedParent;
    }

}