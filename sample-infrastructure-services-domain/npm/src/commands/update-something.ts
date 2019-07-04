import { ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { IUpdateSomethingHandler, UpdateSomethingCommand } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

export class DomainUpdateSomethingHandler extends DomainHandlerBase<UpdateSomethingCommand, number> implements IUpdateSomethingHandler {

    readonly commandType = UpdateSomethingCommand.TYPE;

    constructor(uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    protected async onHandleAsync(uow: ISampleUnitOfWork, command: UpdateSomethingCommand): Promise<number> {
        var parent = await uow.parents.getAsync(command.someId);
        if (!parent) {
            throw new Error(`Could not locate record for id (${command.someId})`);
        }
        parent.addChild(command.someNewData);
        parent.description = command.someNewData;
        await uow.parents.updateAsync(command.someId, parent);
        return parent.id;
    }

}
