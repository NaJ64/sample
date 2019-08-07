import { injectable } from "inversify";
import { ISampleUnitOfWork, ISampleUnitOfWorkFactory } from "sample-domain";
import { ICommand, ICommandHandler } from "sample-services";

@injectable()
export abstract class DomainHandlerBase<TCommand extends ICommand, TResult=void> implements ICommandHandler<TCommand, TResult> {

    protected readonly _uowFactory: ISampleUnitOfWorkFactory;

    constructor(uowFactory: ISampleUnitOfWorkFactory) {
        this._uowFactory = uowFactory;
    }

    abstract readonly commandType: string;

    protected abstract onHandleAsync(uow: ISampleUnitOfWork, command: TCommand): Promise<TResult>;

    async handleAsync(command: TCommand): Promise<TResult> {
        let uow: ISampleUnitOfWork | null = null;
        let transactionId = "";
        let result: TResult;
        try {
            uow = this._uowFactory.create();
            transactionId = await uow.beginAsync();
            result = await this.onHandleAsync(uow, command);
            await uow.commitAsync();
            await uow.disposeAsync();
            return result;
        }
        catch (e) {
            if (uow) {
                transactionId && await uow.rollbackAsync();
                await uow.disposeAsync();
            }
            throw e;
        }
    }

}