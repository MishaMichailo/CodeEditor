﻿namespace Code_editor.Libraries
{
    public class LanguagesDictionary
    {
        private readonly Dictionary<string, string> languages = new Dictionary<string, string>
        {
            { "C", "C" },
            { "C++14", "CPP14" },
            { "C++17", "CPP17" },
            { "Clojure", "CLOJURE" },
            { "C#", "CSHARP" },
            { "Go", "GO" },
            { "Haskell", "HASKELL" },
            { "Java 8", "JAVA8" },
            { "Java 14", "JAVA14" },
            { "JavaScript(Nodejs)", "JAVASCRIPT_NODE" },
            { "Kotlin", "KOTLIN" },
            { "Objective C", "OBJECTIVEC" },
            { "Pascal", "PASCAL" },
            { "Perl", "PERL" },
            { "PHP", "PHP" },
            { "Python 2", "PYTHON" },
            { "Python 3", "PYTHON3" },
            { "Python 3.8", "PYTHON3_8" },
            { "R", "R" },
            { "Ruby", "RUBY" },
            { "Rust", "RUST" },
            { "Scala", "SCALA" },
            { "Swift", "SWIFT" },
            { "TypeScript", "TYPESCRIPT" }
        };

        public List<string> GetLanguagesList()
        {
            return languages.Keys.ToList();
        }

        public string GetLanguageCode(string language)
        {
            if (languages.ContainsKey(language))
            {
                return languages[language];
            }
            return null;
        }
    }
}