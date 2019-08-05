import * as MigrationConnectionOptions from "./typeorm/migration-connection-options";

const command = process.argv[2];
if (!command) {
    console.log("Encountered error: No argument(s) were provided");
    throw new Error("Encountered error: No argument(s) were provided");
}
if (command != "typeorm") {
    console.log("Specify 'typeorm' for TypeORM commands");
    throw new Error("Specify 'typeorm' for TypeORM commands");
}

// TypeORM
if (command == "typeorm") {
    const hasTypeOrmArgs =  process.argv.length > 2;
    const typeormArgs = hasTypeOrmArgs ? process.argv.slice(3) : [];
    console.log(`Running 'typeorm ${typeormArgs.join(' ')}' using connection: `, MigrationConnectionOptions);
    if (hasTypeOrmArgs) {
        process.argv.splice(2);
    }
    process.argv.push("-f", "./dist/typeorm/migration-connection-options");
    process.argv.push(...typeormArgs);
    import("typeorm/cli");
}

