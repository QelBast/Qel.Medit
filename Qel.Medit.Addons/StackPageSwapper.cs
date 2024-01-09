using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Qel.Medit.Addons;

public static class StackPageSwapper
{
    public static async Task PageSwap<T>(this INavigation navigation, Page currentPage) where T : Page, new()
    {
        var pages = navigation.NavigationStack.OfType<T>();
        if (pages.Count() > 1)
        {
            Console.WriteLine("Больше одной страницы");

            foreach (var item in pages)
            {
                navigation.RemovePage(item);
            }
        }
        else if (pages.Count() == 1)
        {
            for (var i = 0; i < navigation.NavigationStack.Count; i++)
            {
                if (navigation.NavigationStack[i].GetType() == typeof(T))
                {
                    var temp = navigation.NavigationStack[i];
                    navigation.RemovePage(navigation.NavigationStack[i]);
                    await navigation.PushAsync(temp, true);
                    break;
                }
            }
        }
        else if (!pages.Any())
        {
            await navigation.PushAsync(new T());
        }
    }

}
