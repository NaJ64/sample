import { inject, injectable } from "inversify";
import { ISampleUnitOfWork, ISampleUnitOfWorkFactory, TYPES as Domain } from "sample-domain";
import { IRemoveSomethingHandler, RemoveSomethingCommand } from "sample-services";
import { DomainHandlerBase } from "../domain-handler";

@injectable()
export class DomainRemoveSomethingHandler extends DomainHandlerBase<RemoveSomethingCommand, number> implements IRemoveSomethingHandler {

    readonly commandType = RemoveSomethingCommand.TYPE;

    constructor(@inject(Domain.Aggregates.ISampleUnitOfWorkFactory) uowFactory: ISampleUnitOfWorkFactory) {
        super(uowFactory);
    }

    async onHandleAsync(uow: ISampleUnitOfWork, command: RemoveSomethingCommand): Promise<number> {
        var parent = await uow.parents.getAsync(command.someId);
        if (!parent) {
            throw new Error(`Could not locate record for id (${command.someId})`);
        }
        await uow.parents.deleteAsync(command.someId);
        return 0;
    }

}
