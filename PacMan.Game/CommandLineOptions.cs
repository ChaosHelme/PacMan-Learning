namespace PacMan.Game;

public enum RenderMode
{
    Ascii,
    Emoji
}

public static class CommandLineOptions
{
    // Extracts the value of --render-mode from args, defaults to Ascii
    public static RenderMode GetRenderMode(string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            // Accepts: --render-mode=emoji or --render-mode emoji
            if (args[i].Equals("--render-mode", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 < args.Length)
                    return ParseRenderMode(args[i + 1]);
            }
            else if (args[i].StartsWith("--render-mode=", StringComparison.OrdinalIgnoreCase))
            {
                var value = args[i].Substring("--render-mode=".Length);
                return ParseRenderMode(value);
            }
        }
        // Default if not specified
        return RenderMode.Ascii;
    }

    private static RenderMode ParseRenderMode(string value)
    {
        return value?.Trim().ToLower() switch
        {
            "emoji" => RenderMode.Emoji,
            _ => RenderMode.Ascii
        };
    }
}