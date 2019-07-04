import { ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { IRemoveSomethingHandler, RemoveSomethingCommand } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

export class DomainRemoveSomethingHandler extends DomainHandlerBase<RemoveSomethingCommand> implements IRemoveSomethingHandler {

    readonly commandType = RemoveSomethingCommand.TYPE;

    constructor(uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    protected async onHandleAsync(uow: ISampleUnitOfWork, command: RemoveSomethingCommand): Promise<void> {
        var parent = await uow.parents.getAsync(command.someId);
        if (!parent) {
            throw new Error(`Could not locate record for id (${command.someId})`);
        }
        await uow.parents.deleteAsync(command.someId);
    }

}
