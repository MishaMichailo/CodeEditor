namespace Code_editor.Libraries
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

        private readonly Dictionary<string, string> initialCodeTemplates = new Dictionary<string, string>
    {
        { "C", "#include <stdio.h>\nint main() {\n    printf(\"Hello, World!\\n\");\n    return 0;\n}" },
        { "C++14", "#include <iostream>\nint main() {\n    std::cout << \"Hello, World!\" << std::endl;\n    return 0;\n}" },
        { "C++17", "#include <iostream>\nint main() {\n    std::cout << \"Hello, World!\" << std::endl;\n    return 0;\n}" },
        { "Clojure", "(println \"Hello, World!\")" },
        { "C#", "using System;\nclass Program {\n    static void Main() {\n        Console.WriteLine(\"Hello, World!\");\n    }\n}" },
        { "Go", "package main\nimport \"fmt\"\nfunc main() {\n    fmt.Println(\"Hello, World!\")\n}" },
        { "Haskell", "main = putStrLn \"Hello, World!\"" },
        { "Java 8", "public class Main {\n    public static void main(String[] args) {\n        System.out.println(\"Hello, World!\");\n    }\n}" },
        { "Java 14", "public class Main {\n    public static void main(String[] args) {\n        System.out.println(\"Hello, World!\");\n    }\n}" },
        { "JavaScript(Nodejs)", "console.log(\"Hello, World!\");" },
        { "Kotlin", "fun main() {\n    println(\"Hello, World!\")\n}" },
        { "Objective C", "#import <Foundation/Foundation.h>\nint main(int argc, const char * argv[]) {\n    @autoreleasepool {\n        NSLog(@\"Hello, World!\");\n    }\n    return 0;\n}" },
        { "Pascal", "program HelloWorld;\nbegin\n  writeln('Hello, World!');\nend." },
        { "Perl", "print \"Hello, World!\\n\";" },
        { "PHP", "<?php\necho \"Hello, World!\";\n?>" },
        { "Python 2", "print 'Hello, World!'" },
        { "Python 3", "print('Hello, World!')" },
        { "Python 3.8", "print('Hello, World!')" },
        { "R", "cat('Hello, World!\\n')" },
        { "Ruby", "puts 'Hello, World!'" },
        { "Rust", "fn main() {\n    println!(\"Hello, World!\");\n}" },
        { "Scala", "object Main extends App {\n    println(\"Hello, World!\")\n}" },
        { "Swift", "print(\"Hello, World!\")" },
        { "TypeScript", "console.log(\"Hello, World!\");" }
    };

        public string GetInitialCodeTemplate(string language)
        {
            if (initialCodeTemplates.ContainsKey(language))
            {
                return initialCodeTemplates[language];
            }
            return string.Empty; 
        }

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
            return "null";
        }
    }
}