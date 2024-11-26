namespace GithubPullRequestReviewer.BusinessLogic
{
    public static class Prompts
    {
        private static readonly string ReviewPromptTemplate = """
            I would like you to succinctly analyze the git diff of the pull request. Please strictly follow these rules:
            1. You must determine whether a piece of code needs to be changed. If a code section does not contain identifiable issues, vulnerabilities, optimizations, or enhancements, you should skip analysis for that section.
            2. If you find any places in code, which should be reworked, then you must determine a category of your analysis.
            There are categories, sorted by the priority:
            A) Issues (contains bugs and potential issues)
            B) Vulnerabilities (contains any security vulnerability)
            C) Optimization (contains code optimization opportunities)
            D) Enhancements (contains suggested code enhancements)
            F) Best Practice (contains any general coding standard adherence issues that may not fit the prioritized categories)
            3. Descriptions should be clear, concise, and actionable, focusing on why a change is needed and suggesting specific improvements when relevant.
            4. You must analyze the code with adherence to best practices.
            5. You must structure your answer in provided JSON format below. Output only valid JSON in a compact, single-line format in the format provided below, without any extra commentary or text:
            {"issues":[{"description":"% TEXT OF ISSUE DESCRIPTION %","file":"% FULL PATH OF THE FILE CONTAINING THE ISSUE %","beginsAtCodeLine":% START LINE NUMBER FOR ISSUE %,"endsAtCodeLine":% END LINE NUMBER FOR ISSUE %,}],"vulnerabilities":[{"description":"% TEXT OF VULNERABILITY DESCRIPTION %","file":"% FULL PATH OF THE FILE CONTAINING THE VULNERABILITY %","beginsAtCodeLine":% START LINE NUMBER OF VULNERABILITY %,"endsAtCodeLine":% END LINE NUMBER OF VULNERABILITY %,}],"optimization":[{"description":"% TEXT OF OPTIMIZATION DESCRIPTION %","file":"% FULL PATH OF THE FILE CONTAINING POTENTIAL OPTIMIZATION %","beginsAtCodeLine":% START LINE NUMBER OF OPTIMIZATION %,"endsAtCodeLine":% END LINE NUMBER OF OPTIMIZATION %,}],"enhancements":[{"description":"% TEXT OF ENHANCEMENT DESCRIPTION %","file":"% FULL PATH OF THE FILE CONTAINING CODE TO BE ENHANCED %","beginsAtCodeLine":% START LINE NUMBER OF ENHANCEMENT %,"endsAtCodeLine":% END LINE NUMBER OF ENHANCEMENT %,}],"bestPractices":[{"description":"% DESCRIPTION OF BEST PRACTICE ISSUE OR SUGGESTION %","file":"% FULL PATH OF FILE WHERE BEST PRACTICE ISSUE OCCURS %","beginsAtCodeLine":% START LINE NUMBER OF BEST PRACTICE ISSUE %,"endsAtCodeLine":% END LINE NUMBER OF BEST PRACTICE ISSUE %}]}

            TEXT in uppercase between % describes what you should input there.
            Each array may contain multiple elements or remain empty.
            Ensure that the JSON output is valid to facilitate structured parsing and that it matches the provided format exactly.
            
            To calculate 'beginsAtCodeLine' and 'endsAtCodeLine' you must analyze diff chunk headers
            Each chunk is prepended by a header enclosed within @@ symbols in git diff. The content of the header is a summary of changes made to the file.
            Example diff chunk header: @@ -34,6 +34,8 @@
            In this header example, 6 lines have been extracted starting from line number 34. Additionally, 8 lines have been added starting at line number 34.
            -------------------

            Pull Request Title: %PR_TITLE%
            Pull Request diff:

            %PR_DIFF%
            """;

        public static readonly string CommentPrompt = """
          You reviewed GitHub Pull Request and provided your analysis.
          You have received a comment from the user associated to your recommendation you gave on certain file.
          I would like you to respond to a user’s comment on a pull request.
          Please follow these rules to ensure a clear, professional, and constructive response:
          1. Begin by acknowledging the user’s comment to show that their input is valued.
          2. If the comment raises an issue or question, provide a concise explanation or clarification.
          3. If the comment suggests a change or improvement:
            A) Agree if the suggestion is valid and explain how it will be addressed.
            B) Politely disagree if the suggestion is not feasible, providing reasons and possible alternatives.
          4. Mention specific lines of code, files, or parts of the pull request related to the comment. Use line numbers or code snippets for clarity.
          5. If further action is required, clearly state what you will do next.
          6. Suggest alternatives or seek clarification if needed.
          7. Keep the response polite and professional, even if you disagree with the comment.
          """;

        public static readonly string CommentPromptModelResponse =
            "Of course! Provide me your commend and I will response you using my best analytical possibilities.";

        public static readonly string CommentRequestTemplate =
            """
            Your review information for particular file:
            File name: %FILE NAME%
            Recommendation content:
            %RECOMMENDATION%
            
            User comment:
            %COMMENT%
            """;

        public static readonly string PullRequestTitlePlaceholder = "%PR_TITLE%";
        public static readonly string PullRequestDiffPlaceholder = "%PR_DIFF%";
        public static readonly string FileNamePlaceholder = "%FILE NAME%";
        public static readonly string RecommendationContentPlaceholder = "%RECOMMENDATION%";
        public static readonly string CommentPlaceholder = "%COMMENT%";

        public static string BuildPromptForReview(string pullRequestTitle, string pullRequestDiff)
        {
            return ReviewPromptTemplate
                .Replace(PullRequestTitlePlaceholder, pullRequestTitle)
                .Replace(PullRequestDiffPlaceholder, pullRequestDiff);
        }

        public static string BuildCommentRequest(string comment, string fileName, string recommendationContent)
        {
            return CommentRequestTemplate
                .Replace(FileNamePlaceholder, fileName)
                .Replace(CommentPlaceholder, comment)
                .Replace(RecommendationContentPlaceholder, recommendationContent);
        }
    }
}
