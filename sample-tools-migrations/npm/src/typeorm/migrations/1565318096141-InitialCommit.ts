import {MigrationInterface, QueryRunner} from "typeorm";

export class InitialCommit1565318096141 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`CREATE TABLE "sample"."parent" ("id" SERIAL NOT NULL, "description" character varying NOT NULL, CONSTRAINT "PK_74d140f36b33ff50562e50a187b" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "sample"."child" ("id" SERIAL NOT NULL, "parentId" integer NOT NULL, "description" character varying NOT NULL, CONSTRAINT "PK_65c27a148223ec05a8fcf62a9e5" PRIMARY KEY ("id"))`);
        await queryRunner.query(`ALTER TABLE "sample"."child" ADD CONSTRAINT "FK_df5863412db4aeeb8175b7771b3" FOREIGN KEY ("parentId") REFERENCES "sample"."parent"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
    }

    public async down(queryRunner: QueryRunner): Promise<any> {
        await queryRunner.query(`ALTER TABLE "sample"."child" DROP CONSTRAINT "FK_df5863412db4aeeb8175b7771b3"`);
        await queryRunner.query(`DROP TABLE "sample"."child"`);
        await queryRunner.query(`DROP TABLE "sample"."parent"`);
    }

}
