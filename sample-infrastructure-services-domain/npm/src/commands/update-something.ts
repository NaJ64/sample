import { inject, injectable } from "inversify";
import { ISampleUnitOfWork, ISampleUnitOfWorkFactory, TYPES as Domain } from "sample-domain";
import { IUpdateSomethingHandler, UpdateSomethingCommand } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

@injectable()
export class DomainUpdateSomethingHandler extends DomainHandlerBase<UpdateSomethingCommand, number> implements IUpdateSomethingHandler {

    readonly commandType = UpdateSomethingCommand.TYPE;

    constructor(@inject(Domain.Aggregates.ISampleUnitOfWorkFactory) uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    async onHandleAsync(uow: ISampleUnitOfWork, command: UpdateSomethingCommand): Promise<number> {
        var parent = await uow.parents.getAsync(command.someId);
        if (!parent) {
            throw new Error(`Could not locate record for id (${command.someId})`);
        }
        parent.addChild(command.someNewData);
        parent.description = command.someNewData;
        var updated = await uow.parents.updateAsync(command.someId, parent);
        return updated.id;
    }

}
