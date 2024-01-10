using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Qel.Medit.Addons;

public static class StackPageSwapper
{
    public static async Task PageSwap<T>(this INavigation navigation) where T : Page, new()
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

    public static async Task ModalPageSwap<T>(this INavigation navigation) where T : Page, new()
    {
        var pages = navigation.ModalStack.OfType<T>();
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
            for (var i = 0; i < navigation.ModalStack.Count; i++)
            {
                if (navigation.ModalStack[i].GetType() == typeof(T))
                {
                    var temp = navigation.ModalStack[i];
                    navigation.RemovePage(navigation.ModalStack[i]);
                    await navigation.PushModalAsync(temp, true);
                    break;
                }
            }
        }
        else if (!pages.Any())
        {
            await navigation.PushModalAsync(new T());
        }
    }

    public static async Task ModalQueueEnd<T>(this INavigation navigation) where T : Page, new()
    {
        await navigation.PopToRootAsync();
    }

}
