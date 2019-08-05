import {MigrationInterface, QueryRunner} from "typeorm";

export class InitialCommit1564976226403 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`CREATE TABLE "parent" ("id" SERIAL NOT NULL, "description" character varying NOT NULL, CONSTRAINT "PK_bf93c41ee1ae1649869ebd05617" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "child" ("id" SERIAL NOT NULL, "parentId" integer NOT NULL, "description" character varying NOT NULL, CONSTRAINT "PK_4609b9b323ca37c6bc435ec4b6b" PRIMARY KEY ("id"))`);
    }

    public async down(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`DROP TABLE "child"`);
        await queryRunner.query(`DROP TABLE "parent"`);
    }

}
