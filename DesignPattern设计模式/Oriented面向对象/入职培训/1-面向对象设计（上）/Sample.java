

public class PrintOS
{
    public static void main(final String[] args)
    {
        String osName = System.getProperty("os.name") ;
        if (osName.equals("SunOS") || osName.equals("Linux"))
        {
            System.out.println("This is a UNIX box and therefore good.") ;
        }
        else if (osName.equals("Windows NT") || osName.equals("Windows 95"))
        {
            System.out.println("This is a Windows box and therefore bad.") ;
        }
        else
        {
            System.out.println("This is not a box.") ;
        }
    }
}

// 复杂设计
//PrintOS.java
public class PrintOS
{
    public static void main(final String[] args)
    {
        System.out.println(OSDiscriminator.getBoxSpecifier().getStatement()) ;
    }
}
//OSDiscriminator.java
public class OSDiscriminator // Factory Pattern
{
    private static java.util.HashMap storage = new java.util.HashMap() ;
 
    public static BoxSpecifier getBoxSpecifier()
    {
        BoxSpecifier value = (BoxSpecifier)storage.get(System.getProperty("os.name")) ;
        if (value == null)
            return DefaultBox.value ;
        return value ;
    }
    public static void register(final String key, final BoxSpecifier value)
    {
        storage.put(key, value) ; // Should guard against null keys, actually.
    }
    static
    {
        WindowsBox.register() ;
        UNIXBox.register() ;
        MacBox.register() ;
    }
}
//BoxSpecifier.java
public interface BoxSpecifier
{
    String getStatement() ;
}
//DefaultBox.java
public class DefaultBox implements BoxSpecifier // Singleton Pattern
{
    public static final DefaultBox value = new DefaultBox () ;
    private DefaultBox() { }
    public String getStatement()
    {
        return "This is not a box." ;
    }
}
//UNIXBox.java
public class UNIXBox implements BoxSpecifier // Singleton Pattern
{
    public static final UNIXBox value = new UNIXBox() ;
    private UNIXBox() { }
    public  String getStatement()
    {
        return "This is a UNIX box and therefore good." ;
    }
    public static final void register()
    {
        OSDiscriminator.register("SunOS", value) ;
        OSDiscriminator.register("Linux", value) ;
    }
}
//WindowsBox.java
public class WindowsBox implements BoxSpecifier  // Singleton Pattern
{
    public  static final WindowsBox value = new WindowsBox() ;
    private WindowsBox() { }
    public String getStatement()
    {
        return "This is a Windows box and therefore bad." ;
    }
    public static final void register()
    {
        OSDiscriminator.register("Windows NT", value) ;
        OSDiscriminator.register("Windows 95", value) ;
    }
}
//MacBox.java
public class MacBox implements BoxSpecifier // Singleton Pattern
{
    public static final MacBox value = new MacBox() ;
    private MacBox() { }
    public  String getStatement()
    {
        return "This is a Macintosh box and therefore far superior." ;
    }
    public static final void register()
    {
        OSDiscriminator.register("Mac OS", value) ;
    }
}

//简单设计？
