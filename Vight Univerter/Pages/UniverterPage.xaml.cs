using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Vight_Univerter
{
    public partial class UniverterPage : ContentPage
    {
        public string basicUnitName { get; set; } = "";
        public string basicUnitSymbol { get; set; } = "";
        private readonly Dictionary<string, double> Units = new Dictionary<string, double>();
        private static readonly object[,] NumUnits = new object[,]
        {
            { "幺","(y" , 1 },
            { "仄","(z", 1e3 },
            { "阿","(a", 1e6 },
            { "飞","(f",1e9 },
            { "皮","(p", 1e12 },
            { "埃","(A", 1e14 },
            { "纳","(n", 1e15 },
            { "微","(u", 1e18 },
            { "忽","(cm", 1e19 },
            { "丝","(dm", 1e20 },
            { "毫","(mm", 1e21 },
            { "厘","(c", 1e22 },
            { "分","(d", 1e23 },
            { "","(", 1e24 },
            { "千","(k", 1e27 },
            { "兆","(M", 1e30 },
            { "吉","(G", 1e33 },
            { "太","(T", 1e36 },
            { "拍","(P", 1e39 },
            { "艾","(E", 1e42 },
            { "泽","(Z", 1e45 },
            { "尧","(Y", 1e48 }
        };  //length: 22, basicUnitValue: 1e24
        private static readonly Dictionary<string, Dictionary<string, double>> OtherUnits = new Dictionary<string, Dictionary<string, double>>
        {
            {
                "长度",
                new Dictionary<string, double>
                {
                    { "普朗克长度(lp)",1.61624e-11 },
                    { "英寸(in)",2.53995e22 },
                    { "寸(c)",3.333333e22 },
                    { "英尺(ft)",3.04794e23 },
                    { "尺(c)",3.333333e23 },
                    { "码(yd)",9.14383e23 },
                    { "英寻(Fa)",1.828766e24 },
                    { "丈(z)",3.333333e24 },
                    { "浪(fl)",2.011643e26 },
                    { "里(l)",5e26 },
                    { "英里(mi)",1.60931e27 },
                    { "海里(kt)",1.853e27 },
                    { "天文单位(AU)", 1.495979e35 },
                    { "光年(ly)", 9.460730e39 },
                    { "秒差距(pc)", 3.085712e40 }
                }
            },  //长度
            {
                "质量",
                new Dictionary<string, double>
                {
                    { "普朗克质量(mp)", 2.17651e19 },
                    { "克拉(Ct)", 2e23 },
                    { "钱(q)", 5e24 },
                    { "两(l)", 5e25 },
                    { "磅(lb)", 4.535924e26 },
                    { "斤(j)", 5e26 },
                    { "吨(t)", 1e30 }
                }
            },  //质量
            {
                "时间",
                new Dictionary<string, double>
                {
                    { "普朗克时间(tp)",1e-19 },
                    { "分钟(min)", 6e25 },
                    { "字(z)", 3e26 },
                    { "刻钟(q)", 9e26 },
                    { "小时(h)", 3.6e27 },
                    { "时辰(dh)", 7.2e27 },
                    { "恒星日(sd)", 8.616410e28 },
                    { "日(d)", 8.64e28 },
                    { "候(h)", 4.32e29 },
                    { "周(w)", 6.048e29 },
                    { "月(M)", 2.592e30 },
                    { "季度(Q)", 7.776e30 },
                    { "交点年(dy)", 2.994797e31 },
                    { "年(略短)(y)", 3.1104e31 },
                    { "回归年(ty)", 3.155693e31 },
                    { "恒星年(sy)", 3.15581e31 },
                    { "年代(D)", 3.1104e32 },
                    { "甲子(jz)", 1.86624e33 },
                    { "世纪(C)", 3.1104e33 },
                    { "千纪(ka)", 3.1104e34 },
                    { "银河年(GY)", 7.46496e39 }
                }
            },  //时间
            {
                "电流",
                new Dictionary<string, double>
                {
                    { "吉尔伯特(WG)", 1.256637e24 },
                    { "绝对安培(aA)", 1e25 },
                    { "静安(statA)",2.997925e33 }
                }
            },   //电流
            {
                "量",
                new Dictionary<string, double>{}
            },   //物质的量
            {
                "温度",
                new Dictionary<string, double>{}
            },   //温度
            {
                "光强",
                new Dictionary<string, double>{}
            }   //光强
        };

        public UniverterPage()
        {
            InitializeComponent();
        }
        internal void InitPickerList()
        {
            int j = 0;
            foreach (var i in OtherUnits[Title])
                while (true)
                    if (i.Value < Convert.ToDouble(NumUnits[j, 2]) || j == 22)
                    {
                        AddKey(i.Key, i.Value);
                        break;
                    }
                    else
                    {
                        AddKey(NumUnits[j, 0] + basicUnitName + NumUnits[j, 1] + basicUnitSymbol + ")", Convert.ToDouble(NumUnits[j, 2]));
                        ++j;
                    }
            while (j != 22)
            {
                AddKey(NumUnits[j, 0] + basicUnitName + NumUnits[j, 1] + basicUnitSymbol + ")", Convert.ToDouble(NumUnits[j, 2]));
                ++j;
            }
        }
        private void AddKey(string key, double value)
        {
            inputUnitPicker.Items.Add(key);
            resultUnitPicker.Items.Add(key);
            Units.Add(key, value);
        }

        private void inputEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Univert();
        }
        private void inputUnitPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Univert();
        }
        private void resultUnitPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Univert();
        }

        //转换
        private void Univert()
        {
            if (inputEntry.Text == null || inputEntry.Text == "" || inputUnitPicker.SelectedItem == null || resultUnitPicker.SelectedItem == null || !Regex.IsMatch(inputEntry.Text, @"^[+-]?\d+[.]?\d*$"))
            {
                resultLabel.Text = "[目标值]";
                return;
            }

            resultLabel.Text = Convert.ToString(Convert.ToDouble(inputEntry.Text) * Units[inputUnitPicker.SelectedItem as string] / Units[resultUnitPicker.SelectedItem as string]);
        }
    }
}