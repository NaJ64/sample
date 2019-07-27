import { IPostgresConnection } from "../postgres-connection";

export interface IOptions { 
    postgres?: IPostgresConnection;
}

export class Options implements IOptions { 
    public postgres?: IPostgresConnection;
}