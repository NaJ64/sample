export interface IOptions { 
    enableQueries: boolean;
    enableCommands: boolean;
}

export class Options implements IOptions { 
    public enableQueries: boolean;
    public enableCommands: boolean;
    constructor() {
        this.enableQueries = true;
        this.enableCommands = true;
    }
}