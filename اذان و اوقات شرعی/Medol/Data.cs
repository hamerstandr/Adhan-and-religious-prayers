using System.Collections.Generic;

namespace اذان_و_اوقات_شرعی
{
    public class Data
    {//http://www.noojum.com/other/astronomy-tools/187-online-tools/6266-longitude-latitude.html
        public List<City> Citys;
        public Data()
        {
            Citys = new List<City>() {
                new City("اراک",49.7, 34.09),
                new City("اردبيل",48.3, 34.09),
                new City("اروميه",45.07, 34.09),
                new City("اصفهان",51.64, 34.09),
                new City("اهواز",48.68, 34.09),
                new City("ايلام",46.42, 34.09),
                new City("بجنورد",57.33, 34.09),
                new City("بندرعباس",56.29, 34.09),
                new City("بوشهر",50.84, 34.09),
                new City("بيرجند",59.21, 34.09),
                new City("تبريز",46.28, 34.09),
                new City("تهران",51.41, 34.09),
                new City("خرم آباد",48.34, 34.09),
                new City("رشت",49.59, 34.09),
                new City("زاهدان",60.86, 34.09),
                new City("زنجان",48.5, 34.09),
                new City("ساري",53.06, 34.09),
                new City("سمنان",53.39, 34.09),
                new City("سنندج",47, 34.09),
                new City("شهرکرد",50.86, 34.09),
                new City("شيراز",52.52, 34.09),
                new City("قزوين",50, 34.09),
                new City("قم",50.8746, 34.6416),
                new City("کرمان",57.06, 34.09),
                new City("کرمانشاه",47.09, 34.09),
                new City("گرگان",54.44, 34.09),
                new City("مشهد",59.58, 34.09),
                new City("همدان",48.52, 34.09),
                new City("ياسوج",51.59, 34.09),
                new City("يزد",54.35, 34.09)
            };


        }
    }
}
