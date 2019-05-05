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

        private readonly Text _text;

        public string Message
        {
            get => _text.text;
            set => _text.text = value;
        }

        private PlayerState _state = PlayerState.Idle;

        public PlayerState State
        {
            get => _state;
            set
            {
                _state = value;
                switch (_state)
                {
                    case PlayerState.Idle:
                        OnIdle();
                        break;
                    case PlayerState.Ready:
                        OnReady();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public readonly Image PickupSukumiImage;
        public readonly ResultText ResultText;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Player(RpsButton rock, RpsButton scissors, RpsButton paper, Image pickupSukumiImage, Text text,
            ResultText resultText)
        {
            rock.player = this;
            rock.sukumi = Sukumi.Rock;

            scissors.player = this;
            scissors.sukumi = Sukumi.Scissors;

            paper.player = this;
            paper.sukumi = Sukumi.Paper;

            PickupSukumiImage = pickupSukumiImage;
            _text = text;
            ResultText = resultText;
        }

        private void OnIdle()
        {
            Message = Constants.WaitingMessage;
            _pickupSukumi = Sukumi.Blank;
            _stopwatch.Stop();
        }

        private void OnReady()
        {
            Message = Constants.ReadyMessage;
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
                case Sukumi.Rock:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.Rock:
                            return null;
                        case Sukumi.Scissors:
                            return playerA;
                        case Sukumi.Paper:
                            return playerB;
                        default:
                            return null;
                    }

                case Sukumi.Scissors:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.Rock:
                            return playerB;
                        case Sukumi.Scissors:
                            return null;
                        case Sukumi.Paper:
                            return playerA;
                        default:
                            return null;
                    }

                case Sukumi.Paper:
                    switch (playerB.PickupSukumi)
                    {
                        case Sukumi.Rock:
                            return playerA;
                        case Sukumi.Scissors:
                            return playerB;
                        case Sukumi.Paper:
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