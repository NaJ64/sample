import { injectable } from "inversify";
import { IEntity, IUnitOfWork, IUnitOfWorkFactory } from "sample-domain";
import { Connection, ConnectionManager, ConnectionOptions, EntitySchema, getConnectionManager, QueryRunner, Repository } from "typeorm";

export abstract class UnitOfWorkBase implements IUnitOfWork {

    private readonly _getConnection: () => Connection;
    private _queryRunner: QueryRunner | null;
    private _transactionId: string | null;

    constructor(getConnection: () => Connection) {
        this._getConnection = getConnection;
        this._queryRunner = null;
        this._transactionId = null;
    }

    private static getTransactionId(): string {
        return Date.now().toLocaleString();
    }
    
    private transactionIsOpen() {
        return (this._transactionId && this._queryRunner && this._queryRunner.isTransactionActive);
    }

    private verifyTransactionOpen() {
        this.verifyNotReleased();
        if (!this.transactionIsOpen()) {
            throw new Error("Must begin a new unit-of-work")
        } 
    }

    private verifyTransactionClosed() {
        if (this.transactionIsOpen()) {
            throw new Error("A unit-of-work has already begun");
        } 
    }

    private verifyNotReleased() {
        if (!this._queryRunner || this._queryRunner.isReleased) {
            throw new Error("Unit-of-work has been disposed or is no longer active");
        } 
    }

    private async connectQueryRunnerAsync(): Promise<void> {
        if (!this._queryRunner || !this._queryRunner.connection.isConnected) {
            this.dispose();
            const connection = await this._getConnection().connect();
            this._queryRunner = connection.createQueryRunner();
        }
    }

    protected getRepositoryForSchema<TEntity extends IEntity, TSchema extends EntitySchema<TEntity>>(entitySchema: TSchema): Repository<TEntity> {
        this.verifyTransactionOpen();
        return (<QueryRunner>this._queryRunner).manager.getRepository<TEntity>(entitySchema);
    }

    async beginAsync(): Promise<string> {
        this.verifyTransactionClosed();
        await this.connectQueryRunnerAsync();
        await (<QueryRunner>this._queryRunner).startTransaction();
        this._transactionId = UnitOfWorkBase.getTransactionId();
        return Promise.resolve(this._transactionId);
    }    

    async commitAsync(): Promise<void> {
        this.verifyTransactionOpen();
        await (<QueryRunner>this._queryRunner).commitTransaction();
        this._transactionId = null;
    }

    async rollbackAsync(): Promise<void> {
        this.verifyTransactionOpen();
        await (<QueryRunner>this._queryRunner).rollbackTransaction();
        this._transactionId = null;
    }

    dispose(): void {
        new Promise(resolve => {
            if (!this._queryRunner) {
                resolve();
                return;
            }
            if (this.transactionIsOpen()) {
                this._queryRunner.rollbackTransaction()
                    .then(() => (<QueryRunner>this._queryRunner).release())
                    .then(() => resolve());
            }
            resolve();
        }).then(() => {
            this._queryRunner = null;
            this._transactionId = null;
        });
    }

}

@injectable()
export abstract class UnitOfWorkFactoryBase<TUnitOfWork extends IUnitOfWork> implements IUnitOfWorkFactory<TUnitOfWork> {

    private readonly _connectionOptions: ConnectionOptions;
    private readonly _connectionManager: ConnectionManager;

    constructor(connectionOptions: ConnectionOptions) {
        this._connectionOptions = connectionOptions;
        this._connectionManager = getConnectionManager();
        this.getConnection = this.getConnection.bind(this)
    }

    protected abstract createInstance(getConnection: () => Connection): TUnitOfWork;

    private getConnection(): Connection {
        return this._connectionManager.create(this._connectionOptions);
    }

    create(): TUnitOfWork {
        return this.createInstance(this.getConnection);
    }

}