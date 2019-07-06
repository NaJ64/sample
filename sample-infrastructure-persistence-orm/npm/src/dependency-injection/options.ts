export interface IPostgresConnection {
    host: string,
    port: number,
    username: string,
    password: string,
    database: string
}

export interface IOptions { 
    postgres?: IPostgresConnection;
}

export class Options implements IOptions { 
    public postgres?: IPostgresConnection;
}