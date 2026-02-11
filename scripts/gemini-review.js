import fs from "fs";

const apiKey = process.env.GEMINI_API_KEY;
const githubToken = process.env.GITHUB_TOKEN;
const repo = process.env.GITHUB_REPOSITORY;
const prNumber = process.env.PR_NUMBER;

async function run() {
  const diff = fs.readFileSync("pr.diff", "utf8");

  // 🔹 Call Gemini
  const response = await fetch(
    `https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=${apiKey}`,
    {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        contents: [
          {
            parts: [
              {
                text: `You are a senior code reviewer.
Review this PR diff and give:
- Bugs
- Security issues
- Performance concerns
- Code improvements
- Best practice violations

Return concise bullet points in markdown.

PR Diff:
${diff}`
              }
            ]
          }
        ]
      })
    }
  );

  const data = await response.json();
  const review =
    data?.candidates?.[0]?.content?.parts?.[0]?.text ||
    "No review generated.";

  // 🔹 Post comment to PR
  await fetch(`https://api.github.com/repos/${repo}/issues/${prNumber}/comments`, {
    method: "POST",
    headers: {
      "Authorization": `Bearer ${githubToken}`,
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      body: `## 🤖 Gemini Code Review\n\n${review}`
    })
  });

  console.log("✅ Review posted to PR");
}

run();
