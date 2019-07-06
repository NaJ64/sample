import { IPostgresConnection } from "sample-infrastructure-persistence-orm";

export interface IOptions {
    domainCommands?: boolean;
    domainQueries?: boolean;
    postgresConnection?: IPostgresConnection;
}

export class Options implements IOptions {
    public domainCommands?: boolean;
    public domainQueries?: boolean;
    public postgresConnection?: IPostgresConnection;
    constructor() {
        this.domainCommands = true;
        this.domainQueries = true;
    }
}