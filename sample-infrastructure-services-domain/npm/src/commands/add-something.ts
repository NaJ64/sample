import { inject, injectable } from "inversify";
import { ISampleUnitOfWork, ISampleUnitOfWorkFactory, Parent, TYPES as Domain } from "sample-domain";
import { AddSomethingCommand, IAddSomethingHandler } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

@injectable()
export class DomainAddSomethingHandler extends DomainHandlerBase<AddSomethingCommand, number> implements IAddSomethingHandler {

    readonly commandType = AddSomethingCommand.TYPE;

    constructor(@inject(Domain.Aggregates.ISampleUnitOfWorkFactory) uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    protected async onHandleAsync(uow: ISampleUnitOfWork, command: AddSomethingCommand): Promise<number> {
        const newRecord = new Parent(command.someNewData);
        newRecord.addChild(newRecord.description);
        var inserted = await uow.parents.insertAsync(newRecord);
        return inserted.id;
    }

}
