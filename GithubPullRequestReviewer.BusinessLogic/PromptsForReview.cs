namespace GithubPullRequestReviewer.BusinessLogic
{
    public static class PromptsForReview
    {
        public static readonly string ReviewPromptTemplate = """
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
            -------------------

            Pull Request Title: %PR_TITLE%
            Pull Request diff:

            %PR_DIFF%
            """;

        public static readonly string PullRequestTitlePlaceholder = "%PR_TITLE%";
        public static readonly string PullRequestDiffPlaceholder = "%PR_DIFF%";

        public static string BuildPromptForReview(string pullRequestTitle, string pullRequestDiff)
        {

            return ReviewPromptTemplate
                .Replace(PullRequestTitlePlaceholder, pullRequestTitle)
                .Replace(PullRequestDiffPlaceholder, pullRequestDiff);
        }
    }
}
