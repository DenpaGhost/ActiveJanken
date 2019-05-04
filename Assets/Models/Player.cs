using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Models
{
    public class Player
    {
        private Sukumi _pickupSukumi;

        public Sukumi PickupSukumi
        {
            get => _pickupSukumi;
            set
            {
                _pickupSukumi = value;
                _stopwatch.Restart();
            }
        }

        public bool IsEnablePickupSukumi => _stopwatch.ElapsedMilliseconds <= Constants.EnablePickupSukumiTime;
        public Text Text;

        public string message
        {
            get => Text.text;
            set => Text.text = value;
        }

        public readonly Image PickupSukumiImage;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Player(RpsButton rock, RpsButton scissors, RpsButton paper, Image pickupSukumiImage, Text text)
        {
            rock.player = this;
            rock.sukumi = Sukumi.rock;

            scissors.player = this;
            scissors.sukumi = Sukumi.scissors;

            paper.player = this;
            paper.sukumi = Sukumi.paper;

            PickupSukumiImage = pickupSukumiImage;
            Text = text;

            _stopwatch.Start();
        }

        [CanBeNull]
        public static Player Battle(Player playerA, Player playerB)
        {
            if (!(playerA.IsEnablePickupSukumi && playerB.IsEnablePickupSukumi))
            {
                return null;
            }

            switch (playerA.PickupSukumi)
            {
                case Sukumi.rock:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return null;
                        case Sukumi.scissors:
                            return playerA;
                        case Sukumi.paper:
                            return playerB;
                        default:
                            return null;
                    }

                case Sukumi.scissors:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return playerB;
                        case Sukumi.scissors:
                            return null;
                        case Sukumi.paper:
                            return playerA;
                        default:
                            return null;
                    }

                case Sukumi.paper:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.rock:
                            return playerA;
                        case Sukumi.scissors:
                            return playerB;
                        case Sukumi.paper:
                            return null;
                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }
    }
}