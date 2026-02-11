import fs from "fs";

const apiKey = process.env.GEMINI_API_KEY;
const githubToken = process.env.GITHUB_TOKEN;
const repo = process.env.GITHUB_REPOSITORY;
const prNumber = process.env.PR_NUMBER;

async function run() {
  const diff = fs.readFileSync("pr.diff", "utf8");

  console.log("Diff length:", diff.length);

  const response = await fetch(
    `https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=${apiKey}`,
    {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        contents: [
          {
            parts: [
              {
                text: `You are a senior code reviewer. Review this PR diff and give concise bullet point feedback:\n\n${diff}`
              }
            ]
          }
        ]
      })
    }
  );

  const data = await response.json();

  console.log("Gemini full response:", JSON.stringify(data, null, 2));

  if (!data.candidates) {
    console.error("❌ Gemini did not return candidates");
    process.exit(1);
  }

  const review = data.candidates[0].content.parts[0].text;

  await fetch(`https://api.github.com/repos/${repo}/issues/${prNumber}/comments`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${githubToken}`,
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      body: `## 🤖 Gemini Code Review\n\n${review}`
    })
  });

  console.log("✅ Review posted to PR");
}

run();
