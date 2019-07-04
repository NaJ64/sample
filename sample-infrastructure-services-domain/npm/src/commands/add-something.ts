import { ISampleUnitOfWork, ISampleUnitOfWorkFactory, Parent } from "sample-domain";
import { AddSomethingCommand, IAddSomethingHandler } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

export class DomainAddSomethingHandler extends DomainHandlerBase<AddSomethingCommand, number> implements IAddSomethingHandler {

    readonly commandType = AddSomethingCommand.TYPE;

    constructor(uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    protected async onHandleAsync(uow: ISampleUnitOfWork, command: AddSomethingCommand): Promise<number> {
        const newRecord = new Parent(command.someNewData);
        newRecord.addChild(newRecord.description);
        var inserted = await uow.parents.insertAsync(newRecord);
        return inserted.id;
    }

}
