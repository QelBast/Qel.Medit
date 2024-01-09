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
            await navigation.PushAsync(pages.FirstOrDefault());
        }
        else if (!pages.Any())
        {
            navigation.InsertPageBefore(new T(), currentPage);
        }
    }
}
