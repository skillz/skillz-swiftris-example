//
//  MainMenuViewController.swift
//  Skillztris
//
//  Created by Rich Hung on 1/16/19.
//  Copyright © 2019 Richard Hung. All rights reserved.
//

import UIKit
import SpriteKit
import GameplayKit
import Skillz

class MainMenuViewController: UIViewController {
    var skillz: Skillz!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        print("Launching Main Menu")
        skillz = Skillz.skillzInstance()
        //skillz.launch()
        //skillz.getMatchInfo()
        // Do any additional setup after loading the view.
    }
    

    @IBAction func SkillzMenu(_ sender: Any) {
        print("Launching Skillz")
        skillz.launch()
        skillz.getMatchInfo()
        print("Starting Match")
    }
    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
