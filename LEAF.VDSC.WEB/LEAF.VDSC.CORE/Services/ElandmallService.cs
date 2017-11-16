using HtmlAgilityPack;
using LEAF.VDSC.CORE.Dao;
using LEAF.VDSC.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEAF.VDSC.CORE.Services
{
    public interface IElandmallService
    {
        IList<Product> SelectOverProducts(int discount);
        void getDiscountProducts();
    }

    public class ElandmallService : BasicService, IElandmallService
    {
        IElandmallDao elandmallDao;

        private const string basicUrl = "http://www.elandmall.com";

        public ElandmallService(IElandmallDao ElandmallDao)
        {
            elandmallDao = ElandmallDao;
        }

        public IList<Product> SelectOverProducts(int discount)
        {
            return elandmallDao.SelectOverProducts(discount);
        }

        public void getDiscountProducts()
        {
            try
            {
                IList<Product> products = new List<Product>();

                for (int i = 1; i < 5; i++)
                {
                    HtmlNodeCollection luckyDealResults = ParseHTML(basicUrl + "/shop/initShopLuckyDeal.action"
                                          + "?listOnly=Y&conr_set_cmps_no=170800000014077&conr_set_no=170800000001584&rows_per_page=60"
                                          + "&conr_stock_grp_no=1708001584&area_no=D1512000293&undefined=0&&_=1508809137180&page_idx=" + i
                                          , "//span[contains(@class,'p_per')]");

                    if (luckyDealResults != null)
                    {
                        for (int j = 0; j < luckyDealResults.Count; j++)
                        {
                            int discount = Int32.Parse(luckyDealResults[j].InnerText.Replace("%", "").Trim());
                            if (discount >= discountMinimum)
                            {
                                Product product = new Product { Id = Guid.NewGuid(), Discount = discount, Detail = luckyDealResults[j].ParentNode.ParentNode.ParentNode.InnerText };
                                products.Add(product);
                            }
                        }
                    }

                }

                HtmlNodeCollection best100Results = ParseHTML(basicUrl + "/shop/initShopBest100.action"
                                                            , "//span[contains(@class,'p_per')]");
                if (best100Results != null)
                {
                    for (int j = 0; j < best100Results.Count; j++)
                    {
                        int discount = Int32.Parse(best100Results[j].InnerText.Split('%')[0].Trim());
                        if (discount >= discountMinimum)
                        {
                            Product product = new Product { Id = Guid.NewGuid(), Discount = discount, Detail = best100Results[j].ParentNode.ParentNode.ParentNode.InnerText };
                            products.Add(product);
                        }
                    }

                }

                elandmallDao.InsertProducts(products);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
