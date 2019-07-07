import { inject, injectable } from "inversify";
import { ISampleUnitOfWork, ISampleUnitOfWorkFactory, Parent, TYPES as Domain } from "sample-domain";
import { IQueries, ISomethingDto } from "sample-services";

@injectable()
export class DomainQueries implements IQueries {

    private readonly _uowFactory: ISampleUnitOfWorkFactory;

    constructor(@inject(Domain.Aggregates.ISampleUnitOfWorkFactory) uowFactory: ISampleUnitOfWorkFactory) {
        this._uowFactory = uowFactory;
    }

    private static toSomethingDto(parent: Parent): ISomethingDto {
        return {
            someId: parent.id,
            someData: parent.description
        };
    }

    private async inRollbackTransactionAsync<TResult>(asyncOperation: (uow: ISampleUnitOfWork) => Promise<TResult>): Promise<TResult> {
        let uow: ISampleUnitOfWork | null = null;
        let transactionId = "";
        let results: TResult;
        try {
            uow = this._uowFactory.create();
            transactionId = await uow.beginAsync();
            results = await asyncOperation(uow);
        }
        finally {
            if (uow) {
                transactionId && await uow.rollbackAsync();
                uow.dispose();
            }
        }
        return results;
    }

    async getSomethingsAsync(): Promise<ISomethingDto[]> {
        return await this.inRollbackTransactionAsync(async uow => {
            const parents = await uow.parents.getAsync();
            return parents.map(DomainQueries.toSomethingDto);
        });
    }    

    async getSomethingByIdAsync(id: number): Promise<ISomethingDto | null> {
        return await this.inRollbackTransactionAsync(async uow => {
            const parent = await uow.parents.getAsync(id);
            return parent ? DomainQueries.toSomethingDto(parent) : null;
        });
    }

}