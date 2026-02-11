import fs from "fs";

const diff = fs.readFileSync("pr.diff", "utf8");

console.log("==== PR DIFF ====");
console.log(diff);