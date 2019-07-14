import { IPostgresConnection } from "sample-infrastructure-persistence-orm";

export interface IOptions {
    domainCommands?: boolean;
    domainQueries?: boolean;
    postgres?: IPostgresConnection;
}

export class Options implements IOptions {
    public domainCommands?: boolean;
    public domainQueries?: boolean;
    public postgres?: IPostgresConnection;
    constructor() {
        this.domainCommands = true;
        this.domainQueries = true;
    }
}